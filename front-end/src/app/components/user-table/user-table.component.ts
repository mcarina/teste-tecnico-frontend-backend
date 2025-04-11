import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionBtnComponent } from '../action-btn/action-btn.component';

@Component({
  selector: 'app-user-table',
  standalone: true,
  imports: [ 
    CommonModule, 
    ActionBtnComponent 
  ],
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.css']
})
export class UserTableComponent {
  
  users = [
    {
      name: 'João Silva',
      email: 'joao.silva@exemplo.com',
      createdAt: '14/01/2023'
    },
    {
      name: 'Maria Oliveira',
      email: 'maria.oliveira@exemplo.com',
      createdAt: '19/02/2023'
    }
  ];
}
