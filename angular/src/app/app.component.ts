import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { Router } from '@angular/router';
import { AuthService } from './shared/services/auth.service';
import { LOGIN_URL } from './shared/constants/urls.const';

@Component({
  selector: 'app-root',
  template: `
    <router-outlet></router-outlet>
  `,
})
export class AppComponent {
  menuMode = 'static';

  constructor(
    private primengConfig: PrimeNGConfig, 
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.primengConfig.ripple = true;
    document.documentElement.style.fontSize = '14px';
    if(this.authService.isAuthenticated() == false){
      this.router.navigate([LOGIN_URL]);
    }
  }
}
