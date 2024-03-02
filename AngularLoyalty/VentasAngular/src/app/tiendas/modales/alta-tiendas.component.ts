import { Component,Inject,ViewChild } from '@angular/core';
import {FormBuilder,FormGroup,Validators}from "@angular/forms";
import {MatDialogRef,MAT_DIALOG_DATA}from "@angular/material/dialog";
import {MatSnackBar}from "@angular/material/snack-bar";
import { Tiendas } from 'src/app/Interface/models';
import { ApiTiendasService } from 'src/app/services/apiTiendas.service';
import { ApiArticulosService } from 'src/app/services/apiArticulos.service';
import { Articulos } from 'src/app/Interface/models';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {SelectionModel} from '@angular/cdk/collections';
@Component({
  selector: 'app-alta-tiendas',
  templateUrl: './alta-tiendas.component.html',
  styleUrls: ['./alta-tiendas.component.css']
})
export class AltaTiendasComponent {

  @ViewChild(MatPaginator) paginator!: MatPaginator;
displayedColumns: string[] = ['select', 'codigo', 'descripcion'];
dataSource = new MatTableDataSource<Articulos>();
selection = new SelectionModel<Articulos>(true, []);


  formData:FormGroup;
  tituloAccion:string="nuevo";
  botonAccion:string="Guardar";
  dataSourceArt :Articulos[]=[];
constructor(
private dialogReferencia:MatDialogRef<AltaTiendasComponent>,
private fb:FormBuilder,
private _snackBar:MatSnackBar,
private _apiServ:ApiTiendasService,
private _apiServArt:ApiArticulosService,
@Inject(MAT_DIALOG_DATA)public dataDialog: Tiendas
){

  this.formData=this.fb.group({
    Sucursal:['',Validators.required],
    Direccion:['']
    
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

  if(this.selection.selected.length<=0){
  this.mostrarAlerta("Seleccione articulos","");
  return;
  }
 
  

  const modelo:Tiendas={
    idTienda:this.dataDialog?this.dataDialog.idTienda:0,
    sucursal:this.formData.value.Sucursal,
    direccion:this.formData.value.Direccion,
    articulos:this.selection.selected
  }


  this._apiServ.creaActualizaTiendas(modelo).subscribe({
    next:(dataResponse)=>{
      if(this.dataDialog){
          if(this.dataDialog.idTienda>0){
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
      Sucursal:this.dataDialog.sucursal,
      Direccion:this.dataDialog.direccion,
    });
  }
  this.tituloAccion="Editar";
  this.botonAccion="Actualizar";

  this._apiServArt.getArticulos().subscribe({
    next:(dataResponse)=>{
      console.log(dataResponse);
        this.dataSourceArt=dataResponse;
    },
    error:(e)=>{}
  })

}


  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  logSelection() {
    this.selection.selected.forEach(s => console.log(s.codigo));
  }



}

