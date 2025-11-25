// src/app/dashboard/dashboard.routes.ts
import { Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth-guard';

export const TIENDA_ROUTES: Routes = [
  // Redireccion a perfil
  {
    path: '',
    redirectTo: 'perfil',
    pathMatch: 'full'
  },

  // Rutas protegidas

    // { path: 'crear',
    //   loadComponent: () => import('../dashboard/paginas/crear/crear').then(m => m.Crear)
    // },
];
