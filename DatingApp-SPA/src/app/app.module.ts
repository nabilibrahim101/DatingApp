import { BrowserModule } from '@angular/platform-browser'; // The BrowserModule is a built-in module that exports basic directives, pipes and services. Unlike previous versions of Angular, we have to explicitly import those dependencies to be able to use directives like *ngFor or *ngIf in our templates
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
// Every angualr application has to have at least one (app.module) file which is decorated with NgModule
// (ValueComponent) Automatically added for us after creating our (value) folders using the [Generate Component]
// context menu from the extension ()
@NgModule({
   declarations: [ // expects an array of components, directives and pipes that are part of the module
      AppComponent,
      ValueComponent
   ],
   imports: [ // expects an array of modules. Here's where we define the pieces of our puzzle (our application)
      BrowserModule,
      AppRoutingModule,
      HttpClientModule // This provides the HttpClient that allow us to use HTTP GET requests
   ],
   providers: [],
   bootstrap: [ // is where we define the root component of our module. Even though this property is also an array, 99% of the time we are going to define only one component.

      AppComponent//whentheappisloadeditisgoingtobootstrapour[AppComponent]->(app.component.ts)
   ]
})
export class AppModule { }
