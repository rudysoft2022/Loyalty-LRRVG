import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';  
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import {MatGridListModule} from '@angular/material/grid-list';
import { ClientesComponent } from './clientes/clientescomponent';
import { AltaClienteComponent } from './clientes/modales/alta-cliente.component';
import { BajaClienteComponent } from './clientes/modales/baja-cliente.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TiendasComponent } from './tiendas/tiendas.component';
import { AltaTiendasComponent } from './tiendas/modales/alta-tiendas.component';
import { BajaTiendasComponent } from './tiendas/modales/baja-tiendas.component';
import { AdminComponent } from './admin/admin.component';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import { RouterModule  } from "@angular/router";
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './admin/modal/login.component';
import { AltaArticulosComponent } from './articulos/modales/alta-articulos.component';
import { BajaArticulosComponent } from './articulos/modales/baja-articulos.component';
import { ArticulosComponent } from './articulos/articulos.component';
import { VentasComponent } from './venta/venta.component';
import { ArticulosItemComponent } from './venta/articulos-item.component';

@NgModule({
  declarations: [
    AppComponent,    
    ClientesComponent,
    AltaClienteComponent,
    BajaClienteComponent,
    TiendasComponent,
    AltaTiendasComponent,
    BajaTiendasComponent,
    AdminComponent,
    LoginComponent,
    AltaArticulosComponent,
    BajaArticulosComponent,
    ArticulosComponent,
    VentasComponent,
    ArticulosItemComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatSnackBarModule,
    MatIconModule,
    MatDialogModule,
    MatGridListModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    RouterModule,
    AppRoutingModule,
    MatCheckboxModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
