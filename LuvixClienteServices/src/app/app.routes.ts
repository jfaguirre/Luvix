import { Routes } from '@angular/router';
import { Inicio } from './paginas/inicio/inicio';
import { Nosotros } from './paginas/nosotros/nosotros';
import { Contactanos } from './paginas/contactanos/contactanos';
import { Registro } from './paginas/registro/registro';
import { Sesion } from './paginas/sesion/sesion';

export const routes: Routes = [

    { path: '', component:Inicio, title: 'Inicio' },
    { path: 'nosotros', component:Nosotros, title: 'Nosotros' },
    { path: 'contactanos', component:Contactanos, title: 'Contactanos' },
    { path: 'registro', component:Registro, title: 'Registro' },
    { path: 'sesion', component:Sesion, title: 'Sesion' },
];
