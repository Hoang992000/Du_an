   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:TicketManagement.View.ReportTicketManagement.View.Report  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5
      -    'TicketManagement.View.Report.ReportView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:TicketManagement.View.Report     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      6	component clr-namespace:RFIDApp.Components     5
      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��2* �6   .+�5   
   �� $��0��6   $��Danh mục báo cáo��6!      SecondaryColor#��[  69   $
��Bold"�6f   $��30(�6x   $��
15,15,15,0q�6�   �� 5   
   $��1��6   
�������$��1,1,1,1q�66    �� Padding$ 5q�6P   $��10,10,10,10q�6\    �� CornerRadius$ 5,5,5,5��6q   ._�5      ��  %� IsVirtualizing . |�.  $��0q�6Q   ��6e   �� *
ListReport+ �� View 5      �� 5      .P�5      ��  �� Width$ 50��6)   $��#��64    �� CellTemplate 5      �� 5   "   .R�5   &   �� $��18(�6E   ��60   �� *STT+6E   5   #   5      �� 5      $	 500��6)   $��Mô tả��65    5      �� 5   "   .R�5    &   �� $��18(�6M   ��60   �� *Description+6M   5!   #   5#      �� 5$      $	 200��6)   $��Hành động��65    5%      �� 5&   "   .R�5'   &   ��  �� Orientation$ 
Horizontal=�61   .+�5(   *   �� $	��100��61     PrimaryButton#��[ 6=   $��Start��6d   $��10,0,0,0q�6t   ��6�   ��  �� RelativeSource �� 	 �� AncestorType$	 ListViewL�
 �� Path$
 DataContext.StartCommand���6�   �� 5)   '   5*   #   5,      5-      5/      50      �� 51   
   $��2��6   
�������$��1,1,1,1q�66   $ 5q�6P   $��
10,0,10,10q�6\   $ 5,5,5,5��6p   ._�52      #  LiveCharts.Wpf.CartesianChart  -   5        $��cartesianChart��52   !   &   LiveCharts.Wpf.Charts.Base.Chart   Series 67   �� *SeriesCollection+   AxisX 54        LiveCharts.Wpf.Axis     Labels 6   �� *Labels+   	Separator 55        LiveCharts.Wpf.Separator  56         Step$ 1Q�6,   67   58         AxisY 5;           LabelFormatter 6    �� *
YFormatter+6H   5=      5>      5?      5@      