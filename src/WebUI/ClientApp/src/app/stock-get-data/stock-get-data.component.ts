import { Component, OnInit, ViewChild } from '@angular/core';
import { StockClient, StockDataViewModel, StockListViewModel } from '../mortoff-api';
import * as moment from "moment";
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexYAxis,
  ApexXAxis,
  ApexTitleSubtitle,
  ApexTooltip
} from "ng-apexcharts";

@Component({
  selector: 'app-stock-get-data',
  templateUrl: './stock-get-data.component.html',
  styleUrls: ['./stock-get-data.component.scss']
})
export class StockGetDataComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: ChartOptions;
  public stocks: StockListViewModel[] = [];
  public stockData: ChartData[] = [];
  public selectedStock: StockListViewModel;
  public dateRanges: DateRanges[] = [];

  constructor(private _stockClient: StockClient) {
    
  }

  ngOnInit(): void {
    this.loadChart();
    this._stockClient.listStocks().subscribe({
      next: (value) => {
        this.stocks = value;
        if (value.length > 0) {
          this.selectedStock = this.stocks[0];
          this.initData();
        }
      }
    });
  }

  public onStockChange($event: any): void {    
    this.selectedStock = this.stocks.filter(x => x.id == $event.target.value)[0];

    this.initData();
  }

  private populateDateRanges(): void {
    this.dateRanges.length = 0;
    var first = moment(this.selectedStock.from);    
    var last = moment(this.selectedStock.to);

    while (first.isBefore(last)) {
      this.dateRanges.push({ from: first.toDate(), to: first.add(3, "months").toDate(), selected: false })
    }

    if (this.dateRanges[0])
      this.dateRanges[0].selected = true;
  }

  public selectDate(dateRange: DateRanges): void {
    var old = this.dateRanges.find(x => x.selected);

    if (old) {
      old.selected = false;
    }

    var _new = this.dateRanges.find(x => x == dateRange);
    if (_new) {
      _new.selected = true;
      this.loadHistory(_new.from, _new.to);
    }    
  }

  private initData(): void {
    const from: Date = this.selectedStock.from as Date;
    const to: Date = moment(this.selectedStock.from).add(3, "months").toDate();
    this.populateDateRanges();
    this.loadHistory(from, to);
  }

  private loadHistory(from: Date, to: Date): void {
    this._stockClient.getHistory(this.selectedStock.id as number, from, to).subscribe({
      next: (value) => {
        this.stockData = value.map(x => this.mapDataToChart(x));
        this.loadChart();
      }
    });
  }

  private mapDataToChart(data: StockDataViewModel): ChartData {
    var result: ChartData = {
      x: data.date as Date,
      y: [data.open as number, data.high as number, data.low as number, data.close as number]
    };
    return result;
  }

  private loadChart(): void {
    this.chartOptions = {
      series: [
        {
          name: "candle",
          data: this.stockData
        }
      ],
      chart: {
        height: 350,
        type: "candlestick"
      },
      tooltip: {
        enabled: true
      },
      xaxis: {
        type: "category",
        labels: {
          formatter: function (val) {
            return moment(val).format("MMM DD");
          }
        }
      },
      yaxis: {
        tooltip: {
          enabled: true
        }
      }
    };
  }
}

export type ChartData = { x: Date, y: [number, number, number, number] };

export type DateRanges = { from: Date, to: Date, selected: boolean };

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  tooltip: ApexTooltip;
};
