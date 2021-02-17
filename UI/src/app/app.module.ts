import { MatListModule } from '@angular/material/list';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HeaderComponent } from './header/header.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AddRoomsComponent } from "./addroom/AddRooms.component";
import { ListViewComponent } from './list-view/list-view.component';
import { RegisterComponent } from './register/register.component';

import { AuthenticationService } from './services/authentication.service';
import { BookingService } from './services/notes.service';
import { RouterService } from './services/router.service';
import { CanActivateRouteGuard } from './can-activate-route.guard';

// Imports for angular material
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule, MatChipsModule,MatPaginatorModule, MatTableModule, MatSnackBar, MatSnackBarModule} from '@angular/material';
import { ModalModule} from 'ngx-bootstrap/modal'
import { CdkTableModule } from '@angular/cdk/table';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { AuthServiceConfig, GoogleLoginProvider, AuthService  } from 'angularx-social-login';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { DatePipe } from '@angular/common';

export function socialLogins() {    
  const socialConfig = new AuthServiceConfig(    
    [    
     {    
        id: GoogleLoginProvider.PROVIDER_ID,    
        provider: new GoogleLoginProvider('869707780258-b2qthvjscn1bc829ic2gpn61q9ted9rk.apps.googleusercontent.com')    
      }    
    ]    
  );    
  return socialConfig;    
}

// Routers
const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'register',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
 {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [ CanActivateRouteGuard ],
    children: [
      {
        path: '',
        redirectTo: 'addnote',
        pathMatch: 'full'
      },
      {
        path: 'addnote',
        component: AddRoomsComponent
      }
      ,
      {
        path: 'view/listview',
        component: ListViewComponent
      }
     ]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    DashboardComponent,
    AddRoomsComponent,
    ListViewComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatExpansionModule,
    MatCardModule,
    MatDialogModule,
    MatSelectModule,
    MatIconModule,
    MatChipsModule,
    MatListModule,
    MatPaginatorModule,
    MatTableModule,
    CdkTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
    ModalModule.forRoot()
  ],
  providers: [
    AuthenticationService,
    BookingService,
    RouterService,
    CanActivateRouteGuard,
    MatDatepickerModule,  
    DatePipe,
    AuthService,    
    {    
      provide: AuthServiceConfig,    
      useFactory: socialLogins    
    },
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
  ],
  bootstrap: [ AppComponent ]
})

export class AppModule { }
