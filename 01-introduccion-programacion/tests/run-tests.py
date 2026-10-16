from pathlib import Path
import subprocess,json,re,math,tempfile
S=Path(__file__).resolve().parents[1];B=Path(tempfile.mkdtemp(prefix='intro-cpp-'))
compiler='cl';results=[]
for source in S.glob('*/solution.cpp'):
 slug=source.parent.name;exe=B/(slug+'.exe')
 c=subprocess.run([compiler,'/nologo','/std:c++17','/EHsc','/W4','/WX','/utf-8',str(source),'/Fe:'+str(exe),'/Fo:'+str(B/(slug+'.obj'))],capture_output=True,text=True,encoding='utf8',errors='replace')
 if c.returncode:raise RuntimeError(c.stdout+c.stderr)
 if slug=='triangulo-rectangulo':
  cases=[('3 4\n',[5,6,12]),('5 12\n',[13,30,30]),('0.3 0.4\n',[.5,.06,1.2]),('0 4\n',None),('-3 4\n',None),('abc 4\n',None),('1e308 1e308\n',None)]
 elif slug=='piramide-asteriscos':cases=[(str(n)+'\n',n if 1<=n<=60 else None) for n in [1,3,60,-1,0,61]]+[('abc\n',None)]
 elif slug=='busqueda-coincidencias-texto':cases=[('ana\nbanana\n',2),('sol\nSol sol solar\n',2),('x\nabc\n',0),('x\n\n',0),('\nabc\n',None),('ana\n',None)]
 elif slug=='ejercicios-secuenciales':cases=[('5\n',8),('-3\n',0),('9223372036854775804\n',9223372036854775807),('9223372036854775807\n',None),('abc\n',None)]
 else:cases=[(str(n)+'\n',n if 1<=n<=93 else None) for n in [1,2,7,93,0,94]]+[('abc\n',None)]
 for i,(data,expected) in enumerate(cases,1):
  p=subprocess.run([str(exe)],input=data,capture_output=True,text=True,encoding='utf8',errors='replace',timeout=5)
  if expected is None:ok=p.returncode!=0 and 'Error:' in p.stderr
  elif slug=='triangulo-rectangulo':
   actual=[float(x) for x in re.findall(r'(?:Hipotenusa|Area|Perimetro): ([\d.]+)',p.stdout)];ok=p.returncode==0 and len(actual)==3 and all(math.isclose(a,b,abs_tol=1e-6) for a,b in zip(actual,expected))
  elif slug=='piramide-asteriscos':
   actual=p.stdout.split('\n',1)[1];want=''.join(' '*(expected-k)+'*'*(2*k-1)+'\n' for k in range(1,expected+1));ok=p.returncode==0 and actual==want
  elif slug=='busqueda-coincidencias-texto':ok=p.returncode==0 and f'Coincidencias: {expected}\n' in p.stdout
  elif slug=='ejercicios-secuenciales':ok=p.returncode==0 and f'B: {expected}\n' in p.stdout
  else:
   f=[0,1]
   for k in range(2,expected):f.append(f[-1]+f[-2])
   ok=p.returncode==0 and p.stdout.split('\n',1)[1].strip()==' '.join(map(str,f[:expected]))
  results.append({'program':slug,'case':i,'input':data,'status':'PASS' if ok else 'FAIL','exitCode':p.returncode,'stdout':p.stdout,'stderr':p.stderr})
out={'environment':'Windows x64; MSVC; C++17; /W4 /WX','programs':5,'passed':sum(x['status']=='PASS' for x in results),'failed':sum(x['status']=='FAIL' for x in results),'results':results}
(S/'tests').mkdir(exist_ok=True);(S/'tests/results-windows.json').write_text(json.dumps(out,ensure_ascii=False,indent=2)+'\n',encoding='utf8')
print(json.dumps({k:v for k,v in out.items() if k!='results'},ensure_ascii=False))
if out['failed']:raise RuntimeError('Hay casos fallidos')
