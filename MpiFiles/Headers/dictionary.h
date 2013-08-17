#ifndef XML_DICTIONARY_H_INCLUDED
#define XML_DICTIONARY_H_INCLUDED

void* DictionaryAdd(void* key, void* value);
void* DictionaryRemove(void* key);
void* DictionaryRemoveByInt(int key);
void DictionaryTest();

#endif
