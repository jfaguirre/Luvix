// src/app/layout/layout.routes.ts
import { Routes } from '@angular/router';
import { NoAuthGuard } from '../guards/no-auth.guard';

export const PUBLIC_ROUTES: Routes = [
  // Redireccion a inicio
  { path: '', redirectTo: 'inicio', pathMatch: 'full' },

  // Rutas públicas
  {
    path: 'inicio',
    loadComponent: () => import('../layout/paginas/inicio/inicio').then(m => m.Inicio)
  },
  {
    path: 'nosotros',
    loadComponent: () => import('../layout/paginas/nosotros/nosotros').then(m => m.Nosotros),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'contactanos',
    loadComponent: () => import('../layout/paginas/contactanos/contactanos').then(m => m.Contactanos),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'sesion',
    loadComponent: () => import('../layout/paginas/sesion/sesion').then(m => m.Sesion),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'registro',
    loadComponent: () => import('../layout/paginas/registro/registro').then(m => m.Registro),
    canActivate: [NoAuthGuard]
  }
];


