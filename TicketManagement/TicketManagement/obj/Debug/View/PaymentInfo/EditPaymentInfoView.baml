   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:TicketManagement.View.PaymentInfo!TicketManagement.View.PaymentInfo  5   	   7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5   	   ;    5TicketManagement.View.PaymentInfo.EditPaymentInfoView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:TicketManagement.View.PaymentInfo     5   	   7
components clr-namespace:RFIDApp.Components     5   	     � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterScreen=�6-   $	��600��5	       $	��350��6-   $	��350��6<    � Title 6   �� *Title+.��5
      ��  �� CornerRadius$ 7��6    �� Padding$ 30q�6   
������ ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��20(�6/   $
��Bold"�6=   $��0��6O   $��	0,10,0,10q�6\   ��6   �� *Title+6\   �� 5      $��1��6   .+�5      �� $��appId��6      TitleControl#��[  6(   �� 5      $��Bottom=�6     PrimaryColor#��[ 61   $��1q�6]   $ 7��6q   ._�5      �� 
������ $��0q�67   
������   DefaultTextboxStyle#��[ 6e   ��6�   �� *appId+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      5      �� 5      $��2��6   .+�5      �� $��merchantName��6   #��[  6/   �� 5      $��Bottom=�6   #��[ 61   $��1q�6]   $ 7��6q   ._�5       �� 
������ $��0q�67   
������ #��[ 6e   ��6�   �� *merchantName+$ TwoWay=�$ PropertyChanged=�5!      5#      �� 5$      $��3��6   .+�5%      �� $��merchantType��6   #��[  6/   �� 5&      $��Bottom=�6   #��[ 61   $��1q�6]   $ 7��6q   ._�5'      �� 
������ $��0q�67   
������ #��[ 6e   ��6�   �� *merchantType+$ TwoWay=�$ PropertyChanged=�5(      5)      �� 5*      $��4��6   .+�5+      �� $��merchantCode��6   #��[  6/   �� 5,      $��Bottom=�6   #��[ 61   $��1q�6]   $ 7��6q   ._�5-      �� 
������ $��0q�67   
������ #��[ 6e   ��6�   �� *merchantCode+$ TwoWay=�$ PropertyChanged=�5.      5/      �� 50      $��5��6   .+�51      �� $��
terminalId��6   #��[  6-   �� 52      $��Bottom=�6   #��[ 61   $��1q�6]   $ 7��6q   ._�53      �� 
������ $��0q�67   
������ #��[ 6e   ��6�   �� *
terminalId+$ TwoWay=�$ PropertyChanged=�54      55      �� 56      $��6��6   .+�57      �� $��	secretKey��6   #��[  6,   ,   &RFIDApp.Components.BindablePasswordBox  58         Password 61   �� *	secretKey+$ PropertyChanged=�	 J� InputBindings	 59      5;      5<      � 5=      $��7��6   $��0,30,0,0q�6    
 � ColumnDefinitions
 5>      �� 5?      $��* �6'   �� 5@      $��* �6'   .+�5B      ��   CancelButton#��[ 6   $��0��6?   ��5C      ��  �� RelativeSource ��  �� AncestorType" ���5D      �� *CancelCommand+�� 5E        
SaveButton#��[ 6   $��1��6=   ��5F      ��  �� " ���5G      �� *SaveCommand+5H      5I      5J      5K      