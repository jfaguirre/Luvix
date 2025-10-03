import { Component, ElementRef, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Menu } from '../menu/menu';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, Menu],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {

}
