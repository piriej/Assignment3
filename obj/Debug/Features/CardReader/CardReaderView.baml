   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Library.ApplicationInfratructure Library.ApplicationInfratructure  5	   	   0    *Library.Features.CardReader.CardReaderView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   D @Prism.Wpf, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null$prismhttp://prismlibrary.com/  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   ?
viewModels.clr-namespace:Library.ApplicationInfratructure  5	   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � Title$ 
CardReader��5   	   $	��300��6   $	��300��6)    � Left$ 50��65   
 � Top$ 50��6?   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *CloseWindowCommand+5      .��5      � .+�5   
   �� -   5         ��Name$ 
TitleLabel��5      $��
CardReader��6$   $��	56,10,0,0q�69      Title-Label#��[  6L   �� 5   
   -   5        $ 
FieldLabel��5      $��Enter Borrower Id : ��6$   $��	62,63,0,0q�6C   �� 5   
   -   5        	 ��Name$	 cardDataBox��5      $��
71,108,0,0q�6'     Device-TextBox#��[ 6�   {�6;   �� *	Enabled+��6Y   �� *
BorrowerId+
 �� UpdateSourceTrigger$
 PropertyChanged=�" �� ValidatesOnNotifyDataErrors . .��6�    =� ErrorTemplate 5      �� 5      .R�5      �� .+�5      ��  ��Name$ textBox��64   �� 5      ��6'   �� ��5      �� 5   "   .R�5   &   �� 
���  ����60   �� *ErrorContent+6N   5   #   5!      5"      5#      .��5$   (   5%      �� 5&   
   -   5         ��Name$ swipeButton��5&      $��
Swipe Card��6&     Standard-Button#��[ 6;   $��
71,198,0,0q�6d   $	��Top=�6x   $	��145��6�   0   *Library.ApplicationInfratructure.ViewNames   ScanBookControl#��Z 6�   {�6�   �� *	Enabled+��6�   �� *CardSwipedCmd+6�   5(      5)      