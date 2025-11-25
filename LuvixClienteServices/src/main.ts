import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { App } from './app/app';
import { routes } from './app/app.routes';
import { authInterceptor } from './app/interceptors/auth-interceptor';
import { errorInterceptor } from './app/interceptors/error-interceptor';

bootstrapApplication(App, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideHttpClient(withInterceptors([authInterceptor, errorInterceptor]))
  ]
})
.catch((err) => console.error(err));
