import { Component,Inject } from '@angular/core';
import {FormBuilder,FormGroup,Validators}from "@angular/forms";
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import {MatSnackBar}from "@angular/material/snack-bar";
import { Login } from 'src/app/Interface/models';
import { ApiClientesService } from 'src/app/services/apiClientes.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {

  formData:FormGroup;
  tituloAccion:string="Login";
  botonAccion:string="Login";

constructor(
private dialogReferencia:MatDialogRef<LoginComponent>,
private fb:FormBuilder,
private _snackBar:MatSnackBar,
private _apiServ:ApiClientesService,
@Inject(MAT_DIALOG_DATA)public dataDialog: Login
){

  this.formData=this.fb.group({
    Correo:['',Validators.required],
    Password:['',Validators.required]   
  })

}

login(){

  const modelo:Login={
    correo:this.formData.value.Correo,
    password:this.formData.value.Password,
  }


  this._apiServ.login(modelo).subscribe({
    next:(dataResponse)=>{
      if(dataResponse){
      this.dialogReferencia.close("success");
        this.mostrarAlerta("Exito","Listo");       
      }
      else
        this.mostrarAlerta("no existe","Error");
      
    },error:(e)=>{
      this.mostrarAlerta("no existe","Error");
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

