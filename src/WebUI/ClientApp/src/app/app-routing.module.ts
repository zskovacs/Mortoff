import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockAddDataComponent } from './stock-add-data/stock-add-data.component';
import { StockGetDataComponent } from './stock-get-data/stock-get-data.component';
import { StockComponent } from './stock/stock.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/stock',
    pathMatch: 'full'
  },
  {
    path: 'stock',
    component: StockComponent
  },
  {
    path: 'stock-add',
    component: StockAddDataComponent
  },
  {
    path: 'stock-get',
    component: StockGetDataComponent
  },
  {
    path: '**',
    redirectTo: '/stock',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
