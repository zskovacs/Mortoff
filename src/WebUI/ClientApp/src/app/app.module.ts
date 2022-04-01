import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AlertModule } from 'ngx-bootstrap/alert';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StockAddDataComponent } from './stock-add-data/stock-add-data.component';
import { StockGetDataComponent } from './stock-get-data/stock-get-data.component';
import { StockComponent } from './stock/stock.component';
import { environment } from '../environments/environment';
import { API_BASE_URL } from './mortoff-api';
import { ServiceProxyModule } from './mortoff-api.module';
import { HttpClientModule } from '@angular/common/http';
import { AlertModalContentComponent } from '../shared/alert-modal/alert-modal.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgApexchartsModule } from 'ng-apexcharts';

@NgModule({
  declarations: [   
    AppComponent,
    StockAddDataComponent,
    StockGetDataComponent,
    StockComponent,
    AlertModalContentComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    ModalModule.forRoot(),
    AlertModule.forRoot(),
    HttpClientModule,
    ServiceProxyModule,
    NgApexchartsModule
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiBaseUrl
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
