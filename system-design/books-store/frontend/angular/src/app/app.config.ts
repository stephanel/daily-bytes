import { ApplicationConfig, ErrorHandler, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { HttpClientModule } from '@angular/common/http';
import { GlobalErrorHandler } from './interceptors/error-handler.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    importProvidersFrom(HttpClientModule),
    { provide: ErrorHandler, useClass: GlobalErrorHandler }
  ]
};
