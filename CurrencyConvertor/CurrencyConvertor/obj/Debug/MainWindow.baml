   M S B A M L   `   `   `  ����L  HCurrencyConvertor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CurrencyConvertorCurrencyConvertor  5   	   "    CurrencyConvertor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CurrencyConvertor     5   	     � Title$  
MainWindow��5   	   $��544.35��6   $��458.1��6,   .��5	      �  J� RenderTransformOrigin$ 0.663,0.434&�6   $��	13,-4,0,0q�6/    � ColumnDefinitions 5
   
   �� 5       � RowDefinitions 5   
   �� 5      $	��39* �6   �� 5      $
��398* �6   �� 5      $	��80* �6   .+�5   
   �� -   5         ��Name$ getExchangeRateButton��5      $��Aktualizovat kurzy��60   $��	10,22,0,0q�6M   $ 0,-2.273&�6`   $��1��6�   $��20��6�   $	��Top=�6�   $
��Left=�6�   $	��155��6�   �� 5   
   -   5         ��Name$ 	textBlock��5      $��154,10,154,0q�6'   $
��Wrap=�6=   $ 0.506,4.688&�6Q   $��18(�6u   $��24��6�   $	��Top=�6�   �� 6�   $�
Konvertor ��6�   �� 6�   $
�měn��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ displayRates_dataGrid��5      $��10,49,17,34q�62   $��1��6G   �� 5   
   -   5        $ exportButton��5      $��Export to XLXS��6'   $��	0,22,17,0q�6@   $ 0.667,-0.227&�6S   $��1��6x   $��20��6�   $	��Top=�6�   $��Right=�6�   $��92��6�   �� 5   
   -   5         ��Name$ convertFrom_comboBox��5      $��
115,14,0,0q�61   $��2��6E   $ 0.699,0.826&�6R   $
��Left=�6v   $��80��6�   $��22��6�   $	��Top=�6�   6  �� 5   
   -   5        $ convertTo_comboBox��5      $��
247,14,0,0q�6/   $��2��6C   $
��Left=�6P   $��80��6k   $��22��6v   $	��Top=�6�   6�   �� 5   
   -   5         ��Name$ setAmount1_textBox��5      $��8,14,0,0q�6.   $��2��6@   	 �� TextWrapping$
	 Wrap=�6M   $
��Left=�6a   $��94��6|   $��23��6�   $	��Top=�6�   
 �� 	MaxLength$
 10��6�   �� 5   
   -   5        $ 
textBlock1��5      $��
200,17,0,0q�6(   $��2��6<   $
��Wrap=�6I   $��  <-->��6]   $
��Left=�6q   $��42��6�   $��16��6�   $	��Top=�6�   �� 5   
   -	   5        $ setAmount2_textBox��5      $��
340,14,0,0q�6.   $��2��6B   $
	 Wrap=�6O   $
��Left=�6c   $��80��6~   $��23��6�   $	��Top=�6�   $
 6��6�   5      5      