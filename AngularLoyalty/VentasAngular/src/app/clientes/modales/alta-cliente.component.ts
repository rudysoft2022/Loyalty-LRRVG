import { Component,Inject } from '@angular/core';
import {FormBuilder,FormGroup,Validators}from "@angular/forms";
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import {MatSnackBar}from "@angular/material/snack-bar";
import { Cliente } from 'src/app/Interface/models';
import { ApiClientesService } from 'src/app/services/apiClientes.service';

@Component({
  selector: 'app-alta-cliente',
  templateUrl: './alta-cliente.component.html',
  styleUrls: ['./alta-cliente.component.css']
})
export class AltaClienteComponent {

  formData:FormGroup;
  tituloAccion:string="nuevo";
  botonAccion:string="Guardar";

constructor(
private dialogReferencia:MatDialogRef<AltaClienteComponent>,
private fb:FormBuilder,
private _snackBar:MatSnackBar,
private _apiServ:ApiClientesService,
@Inject(MAT_DIALOG_DATA)public dataDialog: Cliente
){

  this.formData=this.fb.group({
    Nombre:['',Validators.required],
    Apellidos:[''],
    Direccion:[''],
    Correo:['',Validators.required],
    Password:['',Validators.required]
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
 
  const modelo:Cliente={
    idCliente:this.dataDialog?this.dataDialog.idCliente:0,
    nombre:this.formData.value.Nombre,
    apellidos:this.formData.value.Apellidos,
    direccion:this.formData.value.Direccion,
    correo:this.formData.value.Correo,
    password:this.formData.value.Password,
  }


  this._apiServ.creaActualizaClientes(modelo).subscribe({
    next:(dataResponse)=>{
      if(this.dataDialog){
          if(this.dataDialog.idCliente>0){
            this.mostrarAlerta("editado","Listo");
            this.dialogReferencia.close("editado");
          }
          else
          {
           this.mostrarAlerta("Creado","Listo");
            this.dialogReferencia.close("creado");
        
           }
       }
      else
      {
        this.mostrarAlerta("Creado","Listo");
        this.dialogReferencia.close("creado");
        
      }
      
    },error:(e)=>{
      this.mostrarAlerta("No se creo","Error");
    }
  });
}

ngOnInit():void{
  if(this.dataDialog){
    this.formData.patchValue({
      Nombre:this.dataDialog.nombre,
      Apellidos:this.dataDialog.apellidos,
      Direccion:this.dataDialog.direccion,
      Correo:this.dataDialog.correo,
    });
  }
  this.tituloAccion="Editar";
  this.botonAccion="Actualizar";
}

}

