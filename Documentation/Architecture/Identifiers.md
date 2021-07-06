# Identifiers

_"A very unique identifier."_

The Ubigia identifier is not based on random values but on a hierarchical structure,
composed of both spatial (Storage/Account/Space) and temporal (Era/Period/Moment) components.
The reasoning is that we want to be able to uniquely identifier each and every unique change a person or system will ever need to make.
For this, the spatial (Storage, Account, Space) component provides a distinction per-user and use-case, and the temporal component (Era, Period, Moment) provides uniqueness within a user's storage.


|                               Storage                              	|                               Account                              	|                               Space                              	|                                        Era                                        	|                                      Period                                      	|                                                Moment                                                	|
|:------------------------------------------------------------------:	|:------------------------------------------------------------------:	|:----------------------------------------------------------------:	|:---------------------------------------------------------------------------------:	|:--------------------------------------------------------------------------------:	|:----------------------------------------------------------------------------------------------------:	|
|                             ```GUID```                             	|                             ```GUID```                             	|                            ```GUID```                            	|                                    ```ULONG```                                    	|                                    ```ULONG```                                   	|                                              ```ULONG```                                             	|
| The storage from which <br>the targeted information<br>originated. 	| The account from which <br>the targeted information<br>originated. 	| The space from which <br>the targeted information<br>originated. 	| An incremental number.<br>In its current form it <br>resembles the years. 	        | An incremental number.<br>In its current form it <br>resembles the days of a year.| An incremental number.<br>In its current form it <br>resembles a day divided <br>in ULONG fragments. 	|
|                                                                    	|                                                                    	|                                                                  	|                                                                                   	|                                                                                  	|                                                                                                      	|

## Benefits

The benefits from this identification scheme are that:
- Information can be duplicated on different storage systems without creating conflicts.<br/>
  Even different storage systems can persist information that originated from other storages, users or spaces.<br/>
  This reduces the complexity of synchronization and caching drastically.

- Information persistence can be made very simple and 'polyglot'.<br/>
  In theory any modern storage technology that facilitates parent/child relations should be usable as a backing store for Ubigia data.<br/>
  Examples of this are currently a (NTFS) filesystem implementation and an in-memory store. More implementations are pending.

## Remarks

- There is probably room for improvement by downscaling the Era and Period ```ULONG``` into something better fitting.
- ...
