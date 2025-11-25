// src/app/interceptors/error.interceptor.ts
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((error) => {
      if (error.status === 401) {
        // Token inválido o expirado → cierra sesión y redirige
        localStorage.removeItem('jwt_token');
        router.navigate(['/sesion']);
      }
      return throwError(() => error);
    })
  );
};

// Necesitas importar estos operadores
import { catchError, throwError } from 'rxjs';
