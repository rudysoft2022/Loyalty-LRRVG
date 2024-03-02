import { Component,Inject } from '@angular/core';
import {FormBuilder,FormGroup,Validators}from "@angular/forms";
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import {MatSnackBar}from "@angular/material/snack-bar";
import { Articulos } from 'src/app/Interface/models';
import { ApiArticulosService } from 'src/app/services/apiArticulos.service';

@Component({
  selector: 'app-alta-articulos',
  templateUrl: './alta-articulos.component.html',
  styleUrls: ['./alta-articulos.component.css']
})
export class AltaArticulosComponent {

  formData:FormGroup;
  tituloAccion:string="nuevo";
  botonAccion:string="Guardar";

constructor(
private dialogReferencia:MatDialogRef<AltaArticulosComponent>,
private fb:FormBuilder,
private _snackBar:MatSnackBar,
private _apiServ:ApiArticulosService,
@Inject(MAT_DIALOG_DATA)public dataDialog: Articulos
){

  this.formData=this.fb.group({
    Codigo:['',Validators.required],
    Descripcion:['',Validators.required],
    Precio:[0,Validators.min(1)],
    Stock:[0,Validators.min(1)],
    
  })

}

mostrarAlerta(msg:string,accion:string){
  this._snackBar.open(msg,accion,{
    horizontalPosition:"end",
    verticalPosition:"top",
    duration:3000
  });
}

addEdit(){
 
  const modelo:Articulos={
    codigo:this.formData.value.Codigo,
    descripcion:this.formData.value.Descripcion,
    precio:this.formData.value.Precio,
    stock:this.formData.value.Stock
  }


  this._apiServ.creaActualizaArticulo(modelo).subscribe({
    next:(dataResponse)=>{
   
        this.mostrarAlerta("Creado","Listo");
       this.dialogReferencia.close("creado");

    },error:(e)=>{
      this.mostrarAlerta("No se creo","Error");
    }
  });
}

ngOnInit():void{
  if(this.dataDialog){
    this.formData.patchValue({
      Codigo:this.dataDialog.codigo,
      Descripcion:this.dataDialog.descripcion,
      Precio:this.dataDialog.precio,
      Stock:this.dataDialog.stock
    });
  }
  this.tituloAccion="Editar";
  this.botonAccion="Actualizar";
}

}

