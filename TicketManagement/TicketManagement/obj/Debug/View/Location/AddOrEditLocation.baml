   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:TicketManagement.View.LocationTicketManagement.View.Location  5   	   7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5   	   6    0TicketManagement.View.Location.AddOrEditLocation   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:TicketManagement.View.Location     5   	   6	component clr-namespace:RFIDApp.Components     5   	   -   5          � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterScreen=�6-   $	��200��5	   !   $	��300��61   $	��350��6A   $	��350��6P    � Title 6	   �� *Title+.��5
      ��  �� CornerRadius$ 7��6    �� Padding$ 30q�6   
������ ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��20(�6/   $
��Bold"�6=   $��0��6O   $��	0,10,0,10q�6\   ��6   �� *Title+6\   �� 5      $��2��6   .+�5      �� $��Tên địa điểm��6      TitleControl#��[  6/   �� 5      $��Bottom=�6     PrimaryColor#��[ 61   $��1q�6]   $ 7��6q   ._�5      �� 
������ $��0q�67   
������   DefaultTextboxStyle#��[ 6e   ��6�   �� *LocationName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      5      � 5      $��5��6   $��0,30,0,0q�6     � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5!      ��   CancelButton#��[ 6   $��0��6?   ��5"      �� 	 �� RelativeSource	 �� 
 �� AncestorType"
 ���5#      �� *CancelCommand+�� 5$        
SaveButton#��[ 6   $��1��6=   ��5%      �� 	 �� "
 ���5&      �� *SaveCommand+5'      5(      5)      5*      