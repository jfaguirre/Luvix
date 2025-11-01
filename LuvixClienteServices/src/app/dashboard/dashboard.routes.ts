// src/app/dashboard/dashboard.routes.ts
import { Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth-guard';

export const DASHBOARD_ROUTES: Routes = [
  // Redireccion a perfil
  {
    path: '',
    redirectTo: 'perfil',
    pathMatch: 'full'
  },

  // Rutas protegidas
    { path: 'perfil',
      loadComponent: () => import('../dashboard/paginas/perfil/perfil').then(m => m.Perfil)
    },

    { path: 'tiendas',
      loadComponent: () => import('../dashboard/paginas/tiendas/tiendas').then(m => m.Tiendas)
    },

    { path: 'ventas',
      loadComponent: () => import('../dashboard/paginas/ventas/ventas').then(m => m.Ventas)
    },

    { path: 'crear',
      loadComponent: () => import('../dashboard/paginas/crear/crear').then(m => m.Crear)
    },
];
