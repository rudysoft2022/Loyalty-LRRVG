import { Component,OnInit,Inject, inject } from '@angular/core';
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import { Articulos } from 'src/app/Interface/models';

@Component({
  selector: 'app-baja-articulos',
  templateUrl: './baja-articulos.component.html',
  styleUrls: ['./baja-articulos.component.css']
})
export class BajaArticulosComponent {

  tituloAccion:string="eliminar";
constructor(
  private dialogReferencia:MatDialogRef<BajaArticulosComponent>,  
  @Inject(MAT_DIALOG_DATA)public dataDialog: Articulos
  )
  {}

confirmar(){
  if(this.dataDialog){
    this.dialogReferencia.close("eliminado");
  }
}

}
