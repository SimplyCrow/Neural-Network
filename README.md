# Neural Network
Das ist ein kleines Projekt, das den Lernprozess des echten Lebens durch Evolution simuliert. 
Jeder Kreis muss Energie sammeln, da er jede Sekunde, welche verliert.
Das Gehirn ist ein FNN (feedforward Neural Network).
Wenn ein Kreis genug Energie gesammelt hat, wird er sterben, aber zwei Kinder erzeugen, die jeweils ein leicht unterschiedliches Gehirn haben können.
Diese Änderungen sind zufällig. Damit überlebt nur der mit der besten Einsammeltechnik der Nahrung und wird vielleicht noch besser durch die Änderungen.

Wenn alle sterben sollten, werden wieder 200 Kreise erzeugt, bis irgendwann eine gute Technik gefunden wird.
Es wird alle 7 Sekunden 250 neue Nahrung erzeugt. Jede Nahrung verschwindet aber wieder nach 15 Sekunden nach seiner Erzeugung.

## Programm
Es ist ein Unity Projekt mit der Unity Version 2021.3.13f1. Es ist auch eine kompilierte Version im Build Ordner für Windows 64 Bit.

Links sieht man die Anzahl an lebenden Kreisen. Die Anzahl an Mutationen und Duplikationen, die bisher in dem Zyklus waren. Danach die Anzahl an Zyklen und dann die Anzahl an gestorbenen Kreisen. Und dann die ersten 3 Kreise mit der meisten Energie.

Wenn man mit der Maus auf einen Kreis klickt, kann man die Details zum Kreis sehen und wenn man 1, 2 oder 3 drückt, wird man zu dem erst-, zweit- oder drittbesten Kreis teleportiert. Die Kamera kann durch Halten der linken Maustaste gesteuert werden, und durch Verwenden des Mausrads ist ein Zoomen möglich.
