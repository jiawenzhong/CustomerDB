   M S B A M L   `   `   `  ����E  ACustomerDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CustomerDB
CustomerDB  5   	       CustomerDB.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CustomerDB     5   	   -   5          �Name$  MainWindowView��5   	    � Title$ Customer Information��5	   	   $	��350��6&   $	��525��63   .��5
      �  � DataContext 6   �� *Main+   Locator �� Source# [  .+�5   
   �� $��Customer ID:��6   $
��Left=�6'   $��	27,24,0,0q�6B   $	��Top=�6U   $��81��6m   �� 5   
   $��Name: ��6   $
��Left=�6!   $��	27,78,0,0q�6<   $	��Top=�6O   �� 5   
   $��	Address: ��6   $
��Left=�6$   $��
27,114,0,0q�6?   $	��Top=�6S   �� 5   
   $��City: ��6   $
��Left=�6!   $��
27,148,0,0q�6<   $	��Top=�6P   �� 5   
   $��State: ��6   $
��Left=�6"   $��
27,182,0,0q�6=   $	��Top=�6Q   �� 5   
   $��	ZipCode: ��6   $
��Left=�6$   $��306,182,0,0q�6?   $	��Top=�6T   �� 5   
   $
��Left=�6S   $��23��6n   $��
123,27,0,0q�6z    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6   �� *
CustomerID+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Get Customer��6   $
��Left=�6(   $��
265,27,0,0q�6C   $	��Top=�6W   $��91��6o   ��6z   �� *
GetCommand+�� 5   
   $	��Add��6   $
��Left=�6   $��
27,246,0,0q�6:   $	��Top=�6N   $��75��6f   ��6q   �� *
AddCommand+�� 5   
   $��Modify��65   $
��Left=�6F   $��143,246,0,0q�6a   $	��Top=�6v   $��75��6�   {�6   �� *ModifyEnable+��6�   �� *EditCommand+�� 5   
   $��Delete��66   $
��Left=�6G   $��265,246,0,0q�6b   $	��Top=�6w   $��75��6�   {�6   �� *DeleteEnabled+��6�   �� *DeleteCommand+�� 5   
   $
��Exit��6   $
��Left=�6    $��417,246,0,0q�6;   $	��Top=�6P   $��75��6h   ��6s   �� *ExitCommand+ �� Mode$ OneWay=���5      ��  �� ElementName$ MainWindowView���� 5   
    �� 
IsReadOnly . $
��Left=�6`   $��23��6{   $��
123,78,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��369��6�   ��6$   �� *Name+$ PropertyChanged=�6�   �� 5   
    . $
��Left=�6b   $��23��6}   $��123,114,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��369��6�   ��6$   �� *	Address+$ PropertyChanged=�6�   �� 5   
    . $
��Left=�6_   $��23��6z   $��123,150,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��369��6�   ��6$   �� *City+$ PropertyChanged=�6�   �� 5   
    . $
��Left=�6d   $��123,185,0,0q�6   $	��Top=�6�   $	��120��6�   $��23��6�   ��6$   �� *	StateCode+$ PropertyChanged=�6�   �� 5   
    . $
��Left=�6b   $��23��6}   $��372,185,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6$   �� *	ZipCode+$ PropertyChanged=�6�   5      5      