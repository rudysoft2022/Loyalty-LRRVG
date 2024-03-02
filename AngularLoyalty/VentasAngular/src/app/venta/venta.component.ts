import { Component,  OnInit} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {  Cliente, DetalleVenta, Tiendas, Ventas } from '../Interface/models';
import {FormBuilder,FormGroup,Validators}from "@angular/forms";
import { ApiTiendasService } from '../services/apiTiendas.service';
import { ApiClientesService } from '../services/apiClientes.service';
import { ApiVentasService } from '../services/apiVentas.service';
import {MatDialog} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CarritoService } from '../services/carrito.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-venta',
  templateUrl: './venta.component.html'
})
export class VentasComponent implements OnInit  {

  
  
  cartItems!: DetalleVenta[];
  total: number=0;
  displayedColumns: string[] = ['codigo','descripcion', 'precio', 'cantidad', 'accion'];
  dataSource!: MatTableDataSource<DetalleVenta>;
  formData:FormGroup;
  dataSourceCliente:Cliente[]=[];
  dataSourceTienda:Tiendas[]=[];
  subscription!: Subscription;
  constructor(
    private _apisServicioCli:ApiClientesService,
    private _apisServicioTie:ApiTiendasService,
    private fb:FormBuilder,
    private _snackBar:MatSnackBar,
    private _apisServicioVen:ApiVentasService,
    public dialog: MatDialog,
        private _carrito:CarritoService
        

  ){
    this.formData=this.fb.group({
      tienda:['',Validators.required],
      cliente:['',Validators.required]
    })
  }

  ngOnInit(): void {

    this._apisServicioTie.getTiendas().subscribe({
      next:(dataResponse)=>{
        this.dataSourceTienda=dataResponse;
      }
    });
    this._apisServicioCli.getClientes().subscribe({
      next:(dataResponse)=>{
        this.dataSourceCliente=dataResponse;
      }
    });
   
   
    this.cartItems = this._carrito.getCartItems();
    this.calcTotalCost(this.cartItems);
    this.subscription = this._carrito.cartChanged
      .subscribe(
        (cartItems: DetalleVenta[]) => {
          this.cartItems = cartItems;
          this.dataSource = new MatTableDataSource(cartItems);
          this.calcTotalCost(this.cartItems);
        }
      );
    this.dataSource = new MatTableDataSource(this.cartItems);

}

deleteItemCart(index:number) {
  this._carrito.deleteCartItem(index);
}
calcTotalCost(items:DetalleVenta[]) {
  let total = 0;
  items.forEach((item) => {
      total += (item.precio * item.cantidad);
  });
 this.total = total;
}

onComprar(){
  
  const modelo:Ventas={
    idTienda:this.formData.value.tienda,
    idCliente:this.formData.value.cliente,
   articulos:this.dataSource.data
  }


  this._apisServicioVen.realizaVenta(modelo).subscribe({
    next:(dataResponse)=>{
    
        this.mostrarAlerta("Venta realizada","Listo");
    },error:(e)=>{
      this.mostrarAlerta("No se creo","Error");
    }
  });
}

mostrarAlerta(msg:string,accion:string){
  this._snackBar.open(msg,accion,{
    horizontalPosition:"end",
    verticalPosition:"top",
    duration:3000
  });
}


}

