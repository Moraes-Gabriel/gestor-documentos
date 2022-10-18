import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NavComponent } from './services/nav/nav.component';
import { LoginComponent } from './components/user/login/login.component';
import { CriarDocumentosComponent } from './components/documentos/criar-documentos/criar-documentos.component';
import { NavDocumentosComponent } from './components/shared/nav-documentos/nav-documentos.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { CriarDocumentoPdfComponent } from './components/documentos/criar-documento-pdf/criar-documento-pdf.component';
import { ToastrModule } from 'ngx-toastr';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { DetalheDocumentoComponent } from './components/documentos/detalhe-documento/detalhe-documento.component';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { AlertModule,AlertConfig } from 'ngx-bootstrap/alert';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDatepickerModule, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { NavAdminComponent } from './components/shared/nav-admin/nav-admin.component';
import { DocumentosComponent } from './components/documentos/documentos.component';
import { ModalDocumentoCadastradoComponent } from './components/documentos/modal-documento-cadastrado/modal-documento-cadastrado.component';
import { ConcessaoComponent } from './components/concessao/concessao.component';
import { CriarConcessaoComponent } from './components/concessao/criar-concessao/criar-concessao.component';
import { TabelaConcessaoComponent } from './components/concessao/tabela-concessao/tabela-concessao.component';
import { TipoComponent } from './components/tipo/tipo.component';
import { TabelaTipoComponent } from './components/tipo/tabela-tipo/tabela-tipo.component';
import { CriarTipoComponent } from './components/tipo/criar-tipo/criar-tipo.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabelaDocumentosComponent } from './components/documentos/tabela-documentos/tabela-documentos.component';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material/material.module';
import { LoginGoogleComponent } from './components/google/login/loginGoogle.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavComponent,
    CriarDocumentosComponent,
    NavDocumentosComponent,
    HeaderComponent,
    CriarDocumentoPdfComponent,
    RegistrationComponent,
    UserComponent,
    HomeComponent,
    DetalheDocumentoComponent,
    ModalDocumentoCadastradoComponent,
    NavAdminComponent,
    DocumentosComponent,
    ConcessaoComponent,
    CriarConcessaoComponent,
    TabelaConcessaoComponent,
    TipoComponent,
    TabelaTipoComponent,
    CriarTipoComponent,
    TabelaDocumentosComponent,
    LoginGoogleComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    CommonModule,
    AppRoutingModule,
    FormsModule,
    PaginationModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    PdfViewerModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3500,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    AccordionModule,
    AlertModule,
    ButtonsModule,
    CarouselModule,
    CollapseModule,
    BsDatepickerModule.forRoot(),
    MaterialModule,
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi: true}, AlertConfig, BsDatepickerConfig, BsDropdownConfig],
  bootstrap: [AppComponent]
})
export class AppModule { }
