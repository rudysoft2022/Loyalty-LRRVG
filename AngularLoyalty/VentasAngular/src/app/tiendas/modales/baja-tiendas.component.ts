import { Component,OnInit,Inject, inject } from '@angular/core';
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import { Tiendas } from 'src/app/Interface/models';

@Component({
  selector: 'app-baja-tiendas',
  templateUrl: './baja-tiendas.component.html',
  styleUrls: ['./baja-tiendas.component.css']
})
export class BajaTiendasComponent {

  tituloAccion:string="eliminar";
constructor(
  private dialogReferencia:MatDialogRef<BajaTiendasComponent>,  
  @Inject(MAT_DIALOG_DATA)public dataDialog: Tiendas
  )
  {}

confirmar(){
  if(this.dataDialog){
    this.dialogReferencia.close("eliminado");
  }
}

}
