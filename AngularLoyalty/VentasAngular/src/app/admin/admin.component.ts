import { Component} from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { LoginComponent } from './modal/login.component';
import {MatDialog} from '@angular/material/dialog';
@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
})
export class AdminComponent  {

  isLogin:boolean=false;
  constructor(
    private _router: Router,
    private _ativatedRoute: ActivatedRoute,
    public dialog: MatDialog,

     ){

  }


  onRouterNavigate(menurUrl: string, titulo: string) {
    this._router.navigate(
      [
          menurUrl
        ]
  )
   
}
onSalir(){
  this._router.navigate(
    ['']);
    this.isLogin=false;
}

onLogin(){
  const dialogRef = this.dialog.open(LoginComponent,{
    width:"350px"
  }).afterClosed().subscribe(resultado=>{
    console.log(`Dialog result: ${resultado}`);
    if(resultado==="success"){
      this.isLogin=true;
     this._router.navigate(
    [
      'clientes'
    ]
     );
    };
  });
}
}

