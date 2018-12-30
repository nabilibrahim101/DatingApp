import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
// Every angualr application has to have at least one (app.module) file which is decorated with NgModule
// (ValueComponent) Automatically added for us after creating our (value) folders using the [Generate Component]
// context menu from the extension ()
@NgModule({
   declarations: [
      AppComponent,
      ValueComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule // This provides the HttpClient that allow us to use HTTP GET requests
   ],
   providers: [],
   bootstrap: [
      AppComponent//whentheappisloadeditisgoingtobootstrapour[AppComponent]->(app.component.ts)
   ]
})
export class AppModule { }
