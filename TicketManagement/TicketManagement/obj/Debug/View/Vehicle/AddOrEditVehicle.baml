   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TicketManagement.View.VehicleTicketManagement.View.Vehicle  5   	   4    .TicketManagement.View.Vehicle.AddOrEditVehicle   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:TicketManagement.View.Vehicle     5   	     � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterScreen=�6-   $	��200��5       $	��400��60   $	��350��6@   $	��350��6O    � Title 6   �� *Title+.��5	      ��  �� CornerRadius$ 7��6    �� Padding$ 30q�6   
������ ._�5
   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��20(�6/   $
��Bold"�6=   $��0��6O   $��	0,10,0,10q�6\   ��6   �� *Title+6\   �� 5      $��2��6   .+�5      �� $!��Tên loại phương tiện��6      TitleControl#��[  67   �� 5        DefaultTextboxStyle#��[ 6   ��6G   �� *VehicleName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      $��4��6   .+�5      �� $��Giá vé��6   #��[  6)   �� 5      -   5        $��NumericTextBox��5      #��[ 60   ��6]   �� *Cost+$ TwoWay=�$ PropertyChanged=�5      5      � 5      $��5��6   $��0,30,0,0q�6     � ColumnDefinitions 5      �� 5      $��* �6'   �� 5       $��* �6'   .+�5"      ��   CancelButton#��[ 6   $��0��6?   ��5#      �� 	 �� RelativeSource	 �� 
 �� AncestorType"
 ���5$      �� *CancelCommand+�� 5%        
SaveButton#��[ 6   $��1��6=   ��5&      �� 	 �� "
 ���5'      �� *SaveCommand+5(      5)      5*      5+      