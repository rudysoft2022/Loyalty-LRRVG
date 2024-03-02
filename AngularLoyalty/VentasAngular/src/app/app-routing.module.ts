import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TiendasComponent } from './tiendas/tiendas.component';
import { ClientesComponent } from './clientes/clientescomponent';
import { ArticulosComponent } from './articulos/articulos.component';
import { VentasComponent } from './venta/venta.component';

const routes: Routes = [
  { path: 'tiendas', component: TiendasComponent },
  {path:'clientes',component:ClientesComponent},
  {path:'articulos',component:ArticulosComponent},
  {path:'ventas',component:VentasComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
