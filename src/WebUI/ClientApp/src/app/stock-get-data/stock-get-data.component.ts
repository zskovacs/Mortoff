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
  public loading: boolean;

  constructor(private _stockClient: StockClient) {

  }

  // -----------------------------------------------------------------------------------------------------
  // @ Lifecycle hooks
  // -----------------------------------------------------------------------------------------------------

  ngOnInit(): void {
    // inicialiázljuk a chart adatokat
    this.loadChart();

    this._stockClient.listStocks().subscribe({
      next: (value) => {
        this.stocks = value;
        if (value.length > 0) {
          // ha van elérhető stock, akkor az elsőt autómatikusan kiválasztjuk
          this.selectedStock = this.stocks[0];
          this.initData();
        }
      }
    });
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  public onStockChange($event: any): void {
    this.selectedStock = this.stocks.filter(x => x.id == $event.target.value)[0];
    this.initData();
  }

  public selectDate(dateRange: DateRanges): void {
    var originalSelected = this.dateRanges.find(x => x.selected);

    if (originalSelected) {
      originalSelected.selected = false;
    }

    var newSelected = this.dateRanges.find(x => x == dateRange);
    if (newSelected) {
      newSelected.selected = true;

      this.loadHistory(newSelected.from, newSelected.to);
    }

    // beállítjuk a cachebe
    localStorage.setItem(this.selectedStock.id.toString(), JSON.stringify(dateRange));
  }

  private populateDateRanges(): void {
    this.dateRanges.length = 0;
    var first = moment(this.selectedStock.from);
    var last = moment(this.selectedStock.to);

    while (first.isBefore(last)) {
      this.dateRanges.push({ from: first.toDate(), to: first.add(3, "months").toDate(), selected: false })
    }

    // Cacheből lekérjük, hogy van-e már mentett dátum
    let cachedDate = JSON.parse(localStorage.getItem(this.selectedStock.id.toString()));
    if (cachedDate) {
      // ha van akkor kikeressük, hogy a most betöltöttek között van-e ilyen dátum
      var isInRange = this.dateRanges.find(x => moment(x.from).isSame(cachedDate.from, "day"))
      // ha ez is van, akkor beállítjuk kiválasztottnak
      if (isInRange)
        isInRange.selected = true;
      // ha nincs akkor az első elemet betöltjük a tömbből
    } else if (this.dateRanges[0]) {
      this.dateRanges[0].selected = true;
    }
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Private methods
  // -----------------------------------------------------------------------------------------------------

  private initData(): void {
    let cachedDate = JSON.parse(localStorage.getItem(this.selectedStock.id.toString()));

    let from: Date = this.selectedStock.from as Date;
    let to: Date = moment(this.selectedStock.from).add(3, "months").toDate();

    if (cachedDate) {
      from = moment(cachedDate.from).toDate();
      to = moment(cachedDate.to).toDate();
    }

    this.populateDateRanges();
    this.loadHistory(from, to);
  }

  private loadHistory(from: Date, to: Date): void {
    this.loading = true;
    this._stockClient.getHistory(this.selectedStock.id as number, from, to).subscribe({
      next: (value) => {
        this.stockData = value.map(x => this.mapDataToChart(x));
        this.loadChart();
        this.loading = false;
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
