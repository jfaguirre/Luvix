import { Routes } from '@angular/router';
import { Inicio } from './paginas/inicio/inicio';
import { Nosotros } from './paginas/nosotros/nosotros';
import { Contactanos } from './paginas/contactanos/contactanos';

export const routes: Routes = [

    { path: '', component:Inicio },
    { path: '', component:Nosotros },
    { path: '', component:Contactanos },    
];
