   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TicketManagement.View.PaymentInfo!TicketManagement.View.PaymentInfo  5      =    7TicketManagement.View.PaymentInfo.PaymentInfomationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:TicketManagement.View.PaymentInfo     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Thông tin thanh toán��6!      SecondaryColor#��[  6=   $
��Bold"�6j   $��30(�6|   $��
15,15,15,0q�6�   �� 5   
   $��2��6   
�������$��1,1,1,1q�66    �� Padding$ 5q�6P   $��10,10,10,10q�6\    �� CornerRadius$ 5,5,5,5��6q   ._�5      ��  %� IsVirtualizing . |�.  $��0q�6Q   ��6e   �� *
ListInfo+ �� View 5      �� 5      .P�5      ��  �� Width$ 50��6)   $��#��64    �� CellTemplate 5      �� 5   "   .R�5   &   �� $��18(�6E   ��60   �� *STT+6E   5   #   5      �� 5      $	 120��6)   $��appId��65    5      �� 5   "   .R�5   &   �� $��18(�6G   ��60   �� *appId+6G   5    #   5"      �� 5#      $	 150��6)   $��merchantName��65    5$      �� 5%   "   .R�5&   &   �� $��18(�6N   ��60   �� *merchantName+6N   5'   #   5)      �� 5*      $	 150��6)   $��merchantType��65    5+      �� 5,   "   .R�5-   &   �� $��18(�6N   ��60   �� *merchantType+6N   5.   #   50      �� 51      $	 150��6)   $��merchantCode��65    52      �� 53   "   .R�54   &   �� $��18(�6N   ��60   �� *merchantCode+6N   55   #   57      �� 58      $	 150��6)   $��
terminalId��65    59      �� 5:   "   .R�5;   &   �� $��18(�6L   ��60   �� *
terminalId+6L   5<   #   5>      �� 5?      $	 500��6)   $��Hành động��65    5@      �� 5A   "   .R�5B   &   ��  �� Orientation$ 
Horizontal=�61   .+�5C   *   ��   
EditButton#��[ 61   $��10,0,0,0q�6U   ��6g   ��  �� RelativeSource �� 	 �� AncestorType$	 ListViewL�
 �� Path$
 DataContext.EditCommand���6�   �� 5D   '   5E   #   5G      5H      5J      5K      5L      5M      