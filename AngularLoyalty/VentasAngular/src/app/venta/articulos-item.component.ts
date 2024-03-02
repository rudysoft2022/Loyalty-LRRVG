import { Component,  OnInit} from '@angular/core';
import { DetalleVenta,Articulos } from '../Interface/models';
import { ApiArticulosService } from '../services/apiArticulos.service';
import {MatDialog} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import {FormBuilder,FormGroup,Validators}from "@angular/forms";
import { CarritoService } from '../services/carrito.service';



@Component({
  selector: 'app-articulos-item',
  templateUrl: './articulos-item.component.html'
})
export class ArticulosItemComponent implements OnInit  {

  precio: number=0;
  stock:number=0;
  items!: Articulos[];
  cartItem!: FormGroup;
  descripcion!:string;
  constructor(
    private _apisServicioArt:ApiArticulosService,
    public dialog: MatDialog,
    private fb:FormBuilder,
    private _carrito:CarritoService

  ){
    this.cartItem=this.fb.group({
      codigo:['',Validators.required],
      cantidad:[0,Validators.min(1)],
      
    })
  }

  ngOnInit(): void {

    this._apisServicioArt.getArticulos().subscribe({
      next:(dataResponse)=>{
        console.log(dataResponse);
        this.items=dataResponse;
      }
    });
  
  
  }
  
    changeSelection(item:any) {
      this.precio = item.precio;
      this.stock=item.stock;
      this.descripcion=item.descripcion;
    }
  
    addCartItem() {
      const cartItem = this.cartItem.value;
      cartItem.precio = this.precio;
      cartItem.descripcion=this.descripcion;
      this._carrito.addCartItems(cartItem);
      this.cartItem.reset();
    }
}
  


  
