   M S B A M L   `   `   `  ���� V  RPersonalTrainerScheduler.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:PersonalTrainerScheduler.UIPersonalTrainerScheduler.UI  ,    &PersonalTrainerScheduler.UI.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ;local)clr-namespace:PersonalTrainerScheduler.UI     -     � Title$  
MainWindow��$	��550��$	��700�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ 
tabControl��.@��� $��Registration��.���� $��30q�.+�(  "System.Windows.Controls.DatePicker  -   $��registrationDataPicker��$��Center=�$��10q��� $��Center=�.��Time:�� -   $��availibleTimeComboBox��$	��100��$��10q�.@��� .��09: 00�� .��10: 00�� .��11: 00�� .��12: 00�� .��13: 00�� .��14: 00�� .��15: 00�� .��16: 00�� .��17: 00�� .��18: 00�� .��19: 00�� .��20: 00�� .��21: 00�� $��Center=�.��Available trainer:�� -   $��availibleTrainerComboBox��$	��100��$��10q����� .R���  �� Orientation$ 
Horizontal=�.+��� $��0 0 5 0q�����  �� Path$ 	FirstName��� ���� $ LastName��� $��Center=�.��	Customer:�� $ 
Horizontal=�$	��300��.+��� -   $ ��customersSelectionComboBox��$��10 5 5 0q�$	��120��$��25�� �� 
IsReadOnly . ���� .R��� $ 
Horizontal=�.+��� $��0 0 5 0q����� $ 	FirstName��� ���� $ LastName��� $��or��$��Center=�$��5q��� -   $��New Customer��$��8q�$��6q��� $ 
Horizontal=�.+��� -   $#��phoneNumberSelectedCustomerTB��$��	189 0 0 0q��� -	   $��registerTrainingSession��$��33q�$��Center=�$	��150��$��50��.��Register Training Session�� $ 
Horizontal=�$��Center=�$��0 20 0 0q�.+��� $��Current manager :��$��SemiBold"�$��Center=�$��1q��� -
   $��managerFirstNameLB��$��Center=��� -   $��managerLastNameLB��$��Center=�$��2q��� $��Trainers��.��]� .+��� $	��Top=�$��	15 20 0 0q�.��Please select a trainer:�� $	��Top=�$ 
Horizontal=�.+�	� -   $��ByNameRB��$��	20 15 0 0q� d� 	IsChecked$
 TrueY�.��All	� -   $��ByCategoryRB��$��15q�.��By Category�� -   $��
Categories��$��20��$��80��$	��Top=�$��0 15 0 0q����� .R��� ���� *OccupationName+�� -   $��30��$��97��$	��Top=�$��	18 10 0 0q�.��Add new trainer�� $
��Left=�.+�&   System.Windows.Controls.DataGrid  -   $��trainersList��   SelectionMode$ Single=���. $
��Auto=�$��Disabled=�$��
20 30 20 0q�$	��205��$	��164��	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
First Name��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $ 	FirstName�     Width5  /System.Windows.Controls.DataGridLengthConverter$ * $ 	Last Name�� �� $ LastName��� $��5 12 0 0q�$��SemiBold"�.��Current selection:�� $��10 5 0 0q�$ 
Horizontal=�.+��� -   $��25 5 5 5q�$��3q�$��50��.��Modify�� -   $��5q�$��3q�$��50��.��Delete�� $��Right=�$��5,9.6,20,0.4q�.@��� $��Information��$��22��$��-2,-2,-1.6,0q�$��Bottom=�.���� $
��Auto=�$��Disabled=�.��� -   $��trainerInfo��$	��319�� � RowDefinitions �� �� �� �� �� �� ��  � ColumnDefinitions �� $	��39* ��� $	��74* �.+��� $��0��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��
15,14,0,14q�$��81��$��20��First Name:�� $��1��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��
15,14,0,14q�$��78��$��20��
Last Name:�� $��2��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��
15,14,0,14q�$��95��$��20��Date of birth:�� $��3��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,14,0,14.8q�$	��109��$��20��Phone number:�� $��4��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,14.2,0,13.8q�$	��100��$��20��StartOfWork:�� $��5��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,14.2,0,13.8q�$	��100��$��20��
EndOfWork:�� $��6��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,14.2,0,13.8q�$	��100��$��20��Occupations:�� -   $��trainerFirstName��$��0��$��0q� �� 
IsReadOnly . $��1��$	��185��$��20���� -   $��trainerLastName��$��1��$��0q� . $��1��$	��185��$��20���� -   $��trainerDateOfBirth��$��2��$��0q� . $��1��$	��185��$��20���� -   $��trainerPhoneNumber��$��3��$��0q� . $��1��$	��185��$��20���� -   $��trainerStartOfWork��$��4��$��0q� . $��1��$	��185��$��20���� -   $��trainerEndOfWork��$��5��$��0q� . $��1��$	��185��$��20���� -   $��trainerOccupations��$��6��$��0q� . $��1��$��20��$��54.5 14.2 0 13.8q��� $��Schedule��.���� $��5q�.+�  -   $ ��trainersScheduleDataPicker��$��Center=�$��10q�  -   $��scheduleTrainerGrid��$ Single=���. $
��Auto=�$��Disabled=�$��0 25 0 0q�$	��205��$	��330��	 .  
 .    $
 Date�� �� $ TrainingDateTimeStart�  $ Customers Name�� �� $ CustomerEntity.FirstName�  $ * $ Customers Last Name�� �� $ CustomerEntity.LastName��� -   $��
10 15 0 10q�$	��150��$��5q�.��Delete Training Session�� -   $��customersTab��$��	Customers��.���  ��  �� $
��103* ��� $
��126* �.+��� $��20,20,24.6,20q�$��0��$��0��.+��� .��Enter last Name:�� -   $��CustomersSearchTB��  -    $��customersList��$ Single=���. $
��Auto=�$��Disabled=�$��5 20 0 0q�$	��268��$	��245��	 .  
 .    $ 
First Name�� �� $ 	FirstName�  $ * $ 	Last Name�� �� $ LastName��� $��0��$��1��$��1.4,0,0.2,0q�.+��� -!   $��newClientBTN��$��0 40 0 0q�$
��Left=�$	��100�� J� RenderTransformOrigin$ 0.612,2.719&�$��28��.��Add Customer�� $��0 20 0 0q�$��SemiBold"�$��Italic$�.��Current selection:�� .@��� $��Information��.��� -"   $��
clientInfo�� �� �� �� �� ��  �� $��7* ��� $	��10* �.+��� $��0��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,13,0,13.4q�$��81��First Name:�� $��1��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,13.6,0,12.8q�$��78��
Last Name:�� $��2��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,13.2,0,13.2q�$��95��Date of birth:�� $��3��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,12.8,0,13.6q�$	��109��Phone number:�� $��4��$��0��$
��Left=�$��Center=�$��15(�$��SemiBold"�$��15,13.4,0,13q�$��51��	Adress:�� -#   $��customerFirstName��$��0��$��0q� . $��1��$	��120��$��20���� -$   $��customerLastName��$��1��$��0q� . $��1��$	��120��$��20���� -%   $��customerDateOfBirth��$��2��$��0q� . $��1��$	��120��$��20���� -&   $��customerPhoneNumber��$��3��$��0q� . $��1��$	��120��$��20���� -'   $��customerAdress��$��4��$��0q� . $��1��$	��120��$��20���� $��Training Sessions��.��  -(   $��scheduleCustomerGrid��$ Single=���. $
��Auto=�$��Disabled=�$��0 25 0 0q�$	��205��$	��330��	 .  
 .    $
 Date�� �� $ TrainingDateTimeStart�  $ Trainers Name�� �� $ TrainerEntity.FirstName�  $ * $ Trainers Last Name�� �� $ TrainerEntity.LastName��� $ 
Horizontal=�$��	0,0,0.2,0q�.+��� -)   $��
10 15 0 10q�$��5q�.��Modify Customer�� -*   $��
10 15 0 10q�$��5q�.��Delete Customer�� -+   $��
10 15 0 10q�$��5q�.��Delete Training Session