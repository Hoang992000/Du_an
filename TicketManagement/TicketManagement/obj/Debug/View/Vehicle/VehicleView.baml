   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:TicketManagement.View.VehicleTicketManagement.View.Vehicle  5      /    )TicketManagement.View.Vehicle.VehicleView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:TicketManagement.View.Vehicle     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   
�������$��1q�66    �� Padding$ 5q�6J   $��
10,10,10,0q�6V    �� CornerRadius$ 5,5,0,0��6j   $��2��6�   ._�5      ]� .+�5      �� $
��Left=�6   $ ��Danh sách phương tiện��67   $��20(�6T   $��Center=�6b   $
��Bold"�6}      PrimaryColor#��[  6�   �� 5       �� Orientation$ 
Horizontal=�6   $��20,0,0,0q�66   .+�5      ��   PrimaryButton#��[ 6   $��Thêm mới��6D   ��6W   �� *
AddCommand+5      5      5      �� 5   
   $��0��6   $��Phương tiện��6!     SecondaryColor#��[ 64   $
��Bold"�6a   $��30(�6s   $��
15,15,15,0q�6�   �� 5   
   $��2��6   
�������$��1,1,1,1q�66   $ 5q�6P   $��10,10,10,10q�6\   $ 5,5,5,5��6q   ._�5      ��  %� IsVirtualizing . |�.  $��0q�6Q   ��6e   �� *ListVehicle+ �� View 5      �� 5      .P�5      ��  �� Width$ 50��6)   $��#��64    �� CellTemplate 5      �� 5   "   .R�5   &   �� $��18(�6E   ��60   �� *STT+6E   5    #   5"      �� 5#      $	 200��6)   $��Tên phương tiện��65    5$      �� 5%   "   .R�5&   &   �� $��18(�6M   ��60   �� *VehicleName+6M   5'   #   5)      �� 5*      $	 150��6)   $
��Giá��65    5+      �� 5,   "   .R�5-   &   �� $��18(�6F   ��60   �� *Cost+6F   5.   #   50      �� 51      $	 500��6)   $��Hành động��65    52      �� 53   "   .R�54   &   �� $ 
Horizontal=�61   .+�55   *   ��   
EditButton#��[ 61   $��10,0,0,0q�6U   ��6g   ��  �� RelativeSource �� 	 �� AncestorType$	 ListViewL�
 �� Path$
 DataContext.EditCommand���6�   �� �� 56   *   #��[ 61   $��Delete��6X   $��10,0,0,0q�6i   ��6{   ��  �� $	 ListViewL�$
 DataContext.DeleteCommand���6�   �� 57   '   58   #   5:      5;      5=      5>      5?      5@      