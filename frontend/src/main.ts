import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app';   // ✅ IMPORTAR AppComponent

bootstrapApplication(AppComponent, appConfig)   // ✅ ARRANCAR AppComponent
  .catch(err => console.error(err));
