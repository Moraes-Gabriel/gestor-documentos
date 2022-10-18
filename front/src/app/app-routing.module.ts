import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConcessaoComponent } from './components/concessao/concessao.component';
import { CriarConcessaoComponent } from './components/concessao/criar-concessao/criar-concessao.component';
import { TabelaConcessaoComponent } from './components/concessao/tabela-concessao/tabela-concessao.component';
import { CriarDocumentoPdfComponent } from './components/documentos/criar-documento-pdf/criar-documento-pdf.component';
import { CriarDocumentosComponent } from './components/documentos/criar-documentos/criar-documentos.component';
import { DetalheDocumentoComponent } from './components/documentos/detalhe-documento/detalhe-documento.component';
import { DocumentosComponent } from './components/documentos/documentos.component';
import { TabelaDocumentosComponent } from './components/documentos/tabela-documentos/tabela-documentos.component';
import { LoginGoogleComponent } from './components/google/login/loginGoogle.component';
import { HomeComponent } from './components/home/home.component';
import { CriarTipoComponent } from './components/tipo/criar-tipo/criar-tipo.component';
import { TabelaTipoComponent } from './components/tipo/tabela-tipo/tabela-tipo.component';
import { TipoComponent } from './components/tipo/tipo.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { UserComponent } from './components/user/user.component';
import { AuthGuard } from './guard/auth.guard';
import { AuthGuardAdmin } from './guard/auth.guardAdmin';

const routes: Routes = [
  { path: '', redirectTo: 'user/login', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [{
      path: 'documentos', component: DocumentosComponent,
      children: [
        { path: "meus", component: TabelaDocumentosComponent },
        { path: "indice", component: TabelaDocumentosComponent },
        { path: "indice/admin", component: TabelaDocumentosComponent },
        { path: "criar", component: CriarDocumentosComponent },
        { path: "criar/:id", component: CriarDocumentosComponent },
        { path: "editar/:id", component: CriarDocumentosComponent },
        { path: "pdf/:id", component: CriarDocumentoPdfComponent },
        { path: "detalhes/:id", component: DetalheDocumentoComponent },
      ]
    }, { path: 'user', component: UserComponent }

      ,],
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard, AuthGuardAdmin],
    children: [{
      path: 'admin', component: ConcessaoComponent,
      children: [
        { path: 'concessoes/indice', component: TabelaConcessaoComponent },
        { path: 'concessoes/criar', component: CriarConcessaoComponent },
        { path: 'concessoes/criar/:id', component: CriarConcessaoComponent },
        { path: 'tipos/indice', component: TabelaTipoComponent },
        { path: 'tipos/criar', component: CriarTipoComponent },
        { path: 'tipos/criar/:id', component: CriarTipoComponent },
        { path: 'documentos/indice', component: TabelaDocumentosComponent },
      ]
    }],
  },
  {
    path: 'user',
    component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'login/google', component: LoginGoogleComponent},

    ],
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'documentos/meus', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
