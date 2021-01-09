# AI.ComputerVision.DeepFake
DeepFake на языке C#

* shape_predictor_68_face_landmarks.dat.bz2 taken from [here](https://github.com/davisking/dlib-models)

**Message posted [here](https://github.com/davisking/dlib-models) about shape_predictor_68_face_landmarks.dat.bz2**: 

"This is trained on the ibug 300-W dataset (https://ibug.doc.ic.ac.uk/resources/facial-point-annotations/)

C. Sagonas, E. Antonakos, G, Tzimiropoulos, S. Zafeiriou, M. Pantic. 
300 faces In-the-wild challenge: Database and results. 
Image and Vision Computing (IMAVIS), Special Issue on Facial Landmark Localisation "In-The-Wild". 2016.

The license for this dataset excludes commercial use and Stefanos Zafeiriou, one of the creators of the dataset, asked me to include a note here saying that the trained model therefore can't be used in a commerical product. So you should contact a lawyer or talk to Imperial College London to find out if it's OK for you to use this model in a commercial product.

Also note that this model file is designed for use with dlib's HOG face detector. That is, it expects the bounding boxes from the face detector to be aligned a certain way, the way dlib's HOG face detector does it. It won't work as well when used with a face detector that produces differently aligned boxes, such as the CNN based mmod_human_face_detector.dat face detector."



* Изображение мужчины взято с [этого](https://generated.photos) проекта.
* Картина "[Caballero de la mano en el pecho](https://ru.m.wikipedia.org/wiki/%D0%A4%D0%B0%D0%B9%D0%BB:El_caballero_de_la_mano_en_el_pecho.jpg)"
* [Библиотеки](https://github.com/aiframesharp/AIFrameSharpNonCommercialRus)
* [Проект, которым я вдохновляюсь](https://github.com/MarekKowalski/FaceSwap)




Статьи:
* [DeepFake своими руками [часть 1]](https://habr.com/ru/post/470323)


---

* Исходное изображение:

<img width="500" alt="Caballero de la mano en el pecho" src="https://github.com/zaharPonimash/AI.ComputerVision.DeepFake/blob/master/TestApp/TestApp/bin/Debug/data/face.jpg">


* Изображение с лицом: 

<img width="500" alt="Лицо" src="https://github.com/zaharPonimash/AI.ComputerVision.DeepFake/blob/master/TestApp/TestApp/bin/Debug/data/face2.jpg">

* Итоговое изображение:

<img width="500" alt="Outp" src="https://github.com/zaharPonimash/AI.ComputerVision.DeepFake/blob/master/TestApp/TestApp/bin/Debug/output.png">
