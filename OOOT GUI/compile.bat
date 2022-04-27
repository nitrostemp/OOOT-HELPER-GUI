cd OOOT
setup.py -b PAL_1.0 -c
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvarsx86_amd64.bat"
msbuild vs\oot.sln -p:Configuration=Release -p:Platform=Win32
 

  
 