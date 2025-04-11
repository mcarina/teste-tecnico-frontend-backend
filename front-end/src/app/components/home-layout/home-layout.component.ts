import { Component, Input } from '@angular/core';
import { UserTableComponent } from '../user-table/user-table.component';

@Component({
  selector: 'app-home-layout',
  imports: [
    UserTableComponent
  ],
  templateUrl: './home-layout.component.html',
  styleUrl: './home-layout.component.css'
})
export class HomeLayoutComponent {
  @Input() title: string = "";
  @Input() subtitle: string = "";
}
