   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TicketManagement.View.TicketTicketManagement.View.Ticket  5      7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5      .    (TicketManagement.View.Ticket.TicketsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:TicketManagement.View.Ticket     5      6	component clr-namespace:RFIDApp.Components     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Danh sách vé��6!      SecondaryColor#��[  65   $
��Bold"�6b   $��30(�6t   $��
15,15,15,0q�6�   � 5   
   $��4��6   $��10��6D   $��2��6P   $��	10,0,10,0q�6d    � IsIndeterminate .   PrimaryColor#��[ 6�   y�6#   �� *	IsLoading+6�   �� 5   
   $��1��6   
�������$��1q�66    �� Padding$ 5q�6J   $��
10,10,10,0q�6V    �� CornerRadius$ 5,5,0,0��6j   ._�5      ]� .+�5      �� $��Danh sách vé��6   $��20(�60   $��Center=�6>   $��15,0,0,0q�6Y   $
��Bold"�6k   #��[ 6}   &    RFIDApp.Components.CustomTextBox  5         PlaceHolder$" Tìm kiếm theo biển số��6*   $	��200��6N   $��Right=�6Z      Text 6v   �� *	SearchNum+ J� InputBindings 5      �� 5      
 �� Key$ Return��6%    �� Command 62   �� *SearchCommand+5      5      5      �� 5    
   $��2��6   
�������$��1,0,1,1q�66   $��	10,0,10,0q�6P   $ 0,0,5,5��6c   ._�5!      �� $��2q�6   $��0q�6"   |�.  ��66   �� *
ListTicket+��5"      �� 5#        SmallRow	 �� BasedOn#	 [ 6   
 �� 
TargetType"
 ��6@    �� View 5'      �� 5(      .P�5)      ��  �� Width$ 30��6)   $��#��64    �� CellTemplate 5*      �� 5+   "   .R�5,   &   �� ��60   �� *STT+5-   #   5/      �� 50      $	 200��6)   $��Mã vé��65    51      �� 52   "   .R�53   &   �� $��16(�6L   ��60   �� *
TicketCode+6L   54   #   56      �� 57      $	 150��6)   $��Biển số��65    58      �� 59   "   .R�5:   &   �� $��16(�6L   ��60   �� *
VehicleNum+6L   5;   #   5=      �� 5>      $ 80��6)   $��	Loại xe��64    5?      �� 5@   "   .R�5A   &   �� $��16(�6M   ��60   �� *VehicleType+6M   5B   #   5D      �� 5E      $	 200��6)   $��Địa điểm��65    5F      �� 5G   "   .R�5H   &   �� $��16(�6N   ��60   �� *LocationName+6N   5I   #   5K      �� 5L      $	 200��6)   $��Thời gian vào��65    5M      �� 5N   "   .R�5O   &   �� $��16(�6H   ��60   �� *TimeIn+6H   5P   #   5R      �� 5S      $	 200��6)   $��Thời gian ra��65    5T      �� 5U   "   .R�5V   &   �� $��16(�6I   ��60   �� *	TimeOut+6I   5W   #   5Y      �� 5Z      $	 120��6)   $��Thanh toán��65    5[      �� 5\   "   .R�5]   &   �� $��16(�6S   ��60   �� *PaymentMethodType+6S   5^   #   5`      �� 5a      $	 150��6)   $��Trạng thái��65    5b      �� 5c   "   .R�5d   &   �� $��16(�6L   ��60   �� *
StatusOder+��5e   *   �� 5f   .   "
 ��64    �� Triggers 5g   2   �� 5h   6    �� Binding 6B   �� *
StatusOder+ �� Value Đã thanh toán6a   .a�5i   :   ��  �� Property � �� �� Value
 � � �6W   5j   7   �� 5k   6    6B   �� *
StatusOder+ Chưa thanh toán6a   .a�5l   :   ��  � ��
 �  ��6W   5m   7   5o   /   5q   '   5r   #   5t      �� 5u      $	 200��6)   $��Thời gian thanh toán��65    5v      �� 5w   "   .R�5x   &   �� $��16(�6M   ��60   �� *PaymentTime+6M   5y   #   5{      �� 5|      $	 200��6)   $��Giá vé��65    5}      �� 5~   "   .R�5   &   �� $��16(�6F   ��60   �� *Cost+6F   5�   #   5�      5�      5�      5�      &    RFIDApp.Components.PaginationBar  5�   
   $��3��6"   $��10q�6/    � DataContext 6;   �� *
Pagination+5�      5�      