import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { UsuariosComponent } from './Paginas/usuarios/usuarios.component';

import { MatTableModule } from '@angular/material/table';
import {FormsModule} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    UsuariosComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatTableModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    BrowserAnimationsModule   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
