import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { JwtHelper } from 'angular2-jwt'
import { AuthenticationService } from './services/authentication.service';
import { RouterService } from './services/router.service';

@Injectable()
export class CanActivateRouteGuard implements CanActivate {
  constructor(private routerService: RouterService) {
  }
  canActivate() {
    let jwtHelper: JwtHelper = new JwtHelper();
    var isSocial = localStorage.getItem("isSocaialLogin");
    if (isSocial) {
      return true;
    }
    else {
      var token = localStorage.getItem("bearerToken");
      if (token && !jwtHelper.isTokenExpired(token)) {
        return true;
      }
    }
    //this.routerService.routeToLogin();
    //this.routerService.routeToRegister();
    //return true;
  }
}
