import { NgModule } from '@angular/core';
import * as ApiServiceProxies from './mortoff-api';

@NgModule({
  providers: [
    ApiServiceProxies.StockClient
  ]
})
export class ServiceProxyModule { }
