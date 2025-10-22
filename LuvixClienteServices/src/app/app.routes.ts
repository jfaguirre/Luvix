// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth-guard';
import { PUBLIC_ROUTES } from './layout/layout.routes';
import { DASHBOARD_ROUTES } from './dashboard/dashboard.routes';

export const routes: Routes = [

  // Rutas públicas (Layout)
  {
    path: '',
    loadComponent: () => import('./layout/layout').then(c => c.Layout),
    children: PUBLIC_ROUTES
  },

  // Rutas privadas (Dashboard)
  {
    path: 'dashboard',
    loadComponent: () => import('./dashboard/dashboard').then(c => c.Dashboard),
    canActivate: [AuthGuard],
    children: DASHBOARD_ROUTES
  },

  // Redirección
  // { path: '**', redirectTo: '/inicio' }
];
