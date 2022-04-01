import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.scss']
})
export class StockComponent implements OnInit {

  constructor(private _router: Router) { }

  ngOnInit(): void {
  }

  navigate(route: string): void {
    this._router.navigateByUrl(route);
  }

}
