import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

// (platformBrowserDynamic) This is effectively means we're making a web application that runs in a browser, 
// (bootstrapModule) and this is the command that is used to bootstrap the app module which in turn bootstrap the app.component which then loads the HTML on the page.
platformBrowserDynamic().bootstrapModule(AppModule) 
  .catch(err => console.error(err));
