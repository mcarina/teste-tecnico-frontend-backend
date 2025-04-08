import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-login-layout',
  imports: [],
  templateUrl: './login-layout.component.html',
  styleUrl: './login-layout.component.css'
})

export class LoginLayoutComponent {
  @Input() title: string = "";
  @Input() subtitle: string = "";
  @Input() primaryBtn: string = "";
  @Input() secondaryBtn: string = "";
  @Input() disabledPrimaryBtn: boolean = true;

  @Output("submit") onSubmit = new EventEmitter();
  @Output("navigate") onNavigate = new EventEmitter();

  submit(){
    this.onSubmit.emit();
  }

  navigate(){
    this.onNavigate.emit();
  }
}
