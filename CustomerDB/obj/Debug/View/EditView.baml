   M S B A M L   `   `   `  ����E  ACustomerDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CustomerDB.ViewCustomerDB.View  5   	       CustomerDB.View.EditView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CustomerDB.View     5   	   -   5          �Name$  
EditWindow��5   	    � Title$ Edit Customer Information��5	   	   $	��300��6+   $	��450��68   .��5
      �  � DataContext 6   �� *EditViewModel+   Locator �� Source# [  .+�5   
   �� $��Name: ��6   $
��Left=�6!   $��	26,26,0,0q�6<   $	��Top=�6O   �� 5   
   $��	Address: ��6   $
��Left=�6$   $��	26,65,0,0q�6?   $	��Top=�6R   �� 5   
   $��City: ��6   $
��Left=�6!   $��
26,101,0,0q�6<   $	��Top=�6P   �� 5   
   $��State: ��6   $
��Left=�6"   $��
26,143,0,0q�6=   $	��Top=�6Q   �� 5   
   $��	ZipCode: ��6   $
��Left=�6$   $��239,140,-6.2,0q�6?   $	��Top=�6W   �� 5   
   $
��Left=�6(   $��23��6C   $��
114,26,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��292��6�   ��6   �� *Name+6�   �� 5   
   $
��Left=�6+   $��23��6F   $��
114,67,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��292��6�   ��6   �� *	Address+6�   �� 5   
   $
��Left=�6(   $��23��6C   $��114,104,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��292��6�   ��6   �� *City+6�   �� 5   
    �� DisplayMemberPath$ 	StateName��6Z   $
��Left=�6x   $��114,143,0,0q�6�   $	��Top=�6�   $	��120��6�   ��6   �� *States+ �� SelectedItem 62   �� *SelectedState+6�   �� 5   
   $
��Left=�6+   $��23��6F   $��305,140,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��101��6�   ��6   �� *	ZipCode+6�   �� 5   
   $��Accept��6   $
��Left=�6"   $��114,211,0,0q�6=   $	��Top=�6R   $��75��6j   ��6u   �� *AcceptCommand+��6�   ��  �� ElementName$ 
EditWindow���� 5   
   $��Cancel��6   $
��Left=�6"   $��239,211,-20.4,0q�6=   $	��Top=�6V   $��75��6n   ��6y   �� *CancelCommand+ �� Mode$ OneWay=���5      �� $ 
EditWindow��5      5      