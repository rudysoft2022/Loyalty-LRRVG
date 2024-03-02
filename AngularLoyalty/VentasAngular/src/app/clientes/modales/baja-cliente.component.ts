import { Component,OnInit,Inject, inject } from '@angular/core';
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import { Cliente } from 'src/app/Interface/models';

@Component({
  selector: 'app-baja-cliente',
  templateUrl: './baja-cliente.component.html',
  styleUrls: ['./baja-cliente.component.css']
})
export class BajaClienteComponent {

  tituloAccion:string="eliminar";
constructor(
  private dialogReferencia:MatDialogRef<BajaClienteComponent>,  
  @Inject(MAT_DIALOG_DATA)public dataDialog: Cliente
  )
  {}

confirmar(){
  if(this.dataDialog){
    this.dialogReferencia.close("eliminado");
  }
}

}
