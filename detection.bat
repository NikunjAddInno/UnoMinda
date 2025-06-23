@echo off
REM Change directory to where Anaconda is installed
cd C:\Users\Admin\Anaconda3

REM Initialize conda (if necessary)
call C:\Users\Admin\Anaconda3\Scripts\activate.bat

REM Activate the specific environment
call conda activate pytorch

REM Change to the E: drive and the directory where the Python script is located
E:
cd E:\Software\VoltasBeko_Copy_kk\VoltasBeko\Voltas_api_image

REM Run the Python script
python detection.py
